using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using System.Data.SqlClient;
using GMap.NET.WindowsForms.ToolTips;

namespace Gmap_Markers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class CPoint
        {
            public double x { get; set; }
            public double y { get; set; }
            public string place { get; set; }
            public CPoint(double _x, double _y, string _place)
            {
                x = _x;
                y = _y;
                place = _place;
            }
        }

        private bool isLeftButtonDown = false;

        private Timer blinkTimer = new Timer();

        private Gmap_Markers.GMapMarkerImage currentMarker;

        private GMap.NET.WindowsForms.GMapOverlay ListSQL;

        string connString = "Data Source=DESKTOP-RVTJVBF;Initial Catalog=Point_Markers;Integrated Security=True";

        private void Form1_Load(object sender, EventArgs e)
        {
            gMapControl1.Bearing = 0;
 
            gMapControl1.CanDragMap = true;

            gMapControl1.DragButton = MouseButtons.Left;

            gMapControl1.GrayScaleMode = true;

            gMapControl1.MarkersEnabled = true;

            gMapControl1.MaxZoom = 18;

            gMapControl1.MinZoom = 2;

            gMapControl1.MouseWheelZoomType =
                GMap.NET.MouseWheelZoomType.MousePositionAndCenter;

            gMapControl1.NegativeMode = false;

            gMapControl1.PolygonsEnabled = true;

            gMapControl1.RoutesEnabled = true;

            gMapControl1.ShowTileGridLines = false;

            gMapControl1.Zoom = 2;

            gMapControl1.Dock = DockStyle.Fill;

            gMapControl1.MapProvider =
                GMap.NET.MapProviders.GMapProviders.GoogleMap;
            GMap.NET.GMaps.Instance.Mode =
                GMap.NET.AccessMode.ServerOnly;

            GMap.NET.MapProviders.GMapProvider.WebProxy =
                System.Net.WebRequest.GetSystemWebProxy();
            GMap.NET.MapProviders.GMapProvider.WebProxy.Credentials =
                System.Net.CredentialCache.DefaultCredentials;

            gMapControl1.Position = new GMap.NET.PointLatLng(55.75393, 37.620795);
            
            ListSQL =
                new GMap.NET.WindowsForms.GMapOverlay("marker");

            gMapControl1.OnMapZoomChanged += 
                new MapZoomChanged(mapControl_OnMapZoomChanged);
            //gMapControl1.MouseClick += 
            //    new MouseEventHandler(mapControl_MouseClick);
            gMapControl1.MouseDown += 
                new MouseEventHandler(mapControl_MouseDown);
            gMapControl1.MouseUp += 
                new MouseEventHandler(mapControl_MouseUp);
            gMapControl1.MouseMove += 
                new MouseEventHandler(mapControl_MouseMove);
            gMapControl1.OnMarkerClick +=
                new MarkerClick(mapControl_OnMarkerClick);
            gMapControl1.OnMarkerEnter += 
                new MarkerEnter(mapControl_OnMarkerEnter);
            gMapControl1.OnMarkerLeave += 
                new MarkerLeave(mapControl_OnMarkerLeave);

            gMapControl1.Overlays.Add(ListSQL);
            mapView();
        }

        List<CPoint> ListWithSQL = new List<CPoint>();
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;

        double xx, yy;

        void mapControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && isLeftButtonDown)
            {
                if (currentMarker != null)
                {

                    PointLatLng point =
                        gMapControl1.FromLocalToLatLng(e.X, e.Y);
                    currentMarker.Position = point;

                    xx = point.Lat;
                    yy = point.Lng;                  

                    SqlConnection sc = new SqlConnection();
                    SqlCommand com = new SqlCommand();
                    sc.ConnectionString = (connString);
                    sc.Open();

                    com.Connection = sc;
                    com.CommandText = @"UPDATE Place SET Y = @lng, X = @lat Where Place = @place;";
                    com.Parameters.AddWithValue("@lat", xx);
                    com.Parameters.AddWithValue("@place", currentMarker.ToolTipText);
                    com.Parameters.AddWithValue("@lng", yy);

                    com.ExecuteNonQuery();
                    sc.Close();
                }
            }
        }

        void mapControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && isLeftButtonDown)
            {
                isLeftButtonDown = false;
            }
        }

        void mapControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && isLeftButtonDown == false)
            {
                isLeftButtonDown = true;
            }
        }

        void mapControl_OnMarkerLeave(GMapMarker item)
        {
            if (item is GMapMarkerImage)
            {
                currentMarker = null;
                GMapMarkerImage m = item as GMapMarkerImage;
                m.Pen.Dispose();
                m.Pen = null;
            }
        }

        void mapControl_OnMarkerEnter(GMapMarker item)
        {
            if (item is GMapMarkerImage)
            {
                currentMarker = item as GMapMarkerImage;
                currentMarker.Pen = new Pen(Brushes.Red, 2);
            }
        }

        void mapControl_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {           
        }
        
        void mapControl_OnMapZoomChanged()
        {
        }

        private void buttonBeginBlink_Click(object sender, EventArgs e)
        {
            blinkTimer.Interval = 1000;

            blinkTimer.Tick += new EventHandler(blinkTimer_Tick);

            blinkTimer.Start();
        }

        void blinkTimer_Tick(object sender, EventArgs e)
        {
            foreach (GMapMarker m in ListSQL.Markers)
            {
                if (m is GMapMarkerImage)
                {
                    GMapMarkerImage marker = m as GMapMarkerImage;
                    if (marker.OutPen == null)
                        marker.OutPen = new Pen(Brushes.Red, 2);
                    else
                    {
                        marker.OutPen.Dispose();
                        marker.OutPen = null;
                    }
                }
            }
            gMapControl1.Refresh();
        }

        private void buttonStopBlink_Click(object sender, EventArgs e)
        {
            blinkTimer.Stop();
            foreach (GMapMarker m in ListSQL.Markers)
            {
                if (m is GMapMarkerImage)
                {
                    GMapMarkerImage marker = m as GMapMarkerImage;
                    marker.OutPen.Dispose();
                    marker.OutPen = null;
                }
            }
            gMapControl1.Refresh();
        }

        public void mapView()
        {
            gMapControl1.Overlays.Clear();
            sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();

            sqlCommand = new SqlCommand("SELECT * FROM Place", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    ListWithSQL.Add(new CPoint(Convert.ToDouble(sqlDataReader[1]), Convert.ToDouble(sqlDataReader[2]), sqlDataReader[3].ToString()));
                }
            }
            sqlDataReader.Close();

            for (int i = 0; i < ListWithSQL.Count; i++)
            {
                Bitmap bitmap =
                    Bitmap.FromFile(Application.StartupPath + @"\marker.png") as Bitmap;

                GMapMarker marker = new GMapMarkerImage(new PointLatLng(ListWithSQL[i].x, ListWithSQL[i].y), bitmap);
                marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                marker.ToolTipText = ListWithSQL[i].place;
                ListSQL.Markers.Add(marker);
            }
            gMapControl1.Overlays.Add(ListSQL);
            sqlConnection.Close();
            foreach (var item in ListWithSQL)
            {
                Console.WriteLine(item);
            }
        }   
    }
}
