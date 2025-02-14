﻿using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display an Oracle Layer on the map
    /// </summary>
    public partial class DisplayTableFromOracle : IDisposable
    {
        public DisplayTableFromOracle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the Oracle layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            var schoolOverlay = new LayerOverlay();
            MapView.Overlays.Add(schoolOverlay);

            #region Initialize Oracle feature layer

            //// Create the new layer and set the projection as the data is in srid 2276 as our background is srid 3857 (spherical mercator).
            // OracleFeatureLayer schoolLayer = new OracleFeatureLayer(@"OCI:system/ThinkGeodatabasepassword!@sampledatabases.thinkgeo.com/xe", "SCHOOLS", "OGR_FID");
            //OracleFeatureLayer schoolLayer = new OracleFeatureLayer(@"OCI:ThinkGeoSampleUser/ThinkGeoSamplePassword@sampledatabases.thinkgeo.com/xe", "SYSTEM.SCHOOLS", "OGR_FID");
            //schoolLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            //// Add the layer to the overlay we created earlier.
            //schoolOverlay.Layers.Add("Coyote Sightings", schoolLayer);

            //// Set a point style to zoom level 1 and then apply it to all zoom levels up to 20.
            //schoolLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Blue, new GeoPen(GeoColors.White,2));
            //schoolLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            #endregion

            // Set the map view current extent to a bounding box that shows just a few sightings.  
            MapView.CurrentExtent = new RectangleShape(-10789388, 3923878, -10768258, 3906668);

            // Refresh the map.
            await MapView.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}