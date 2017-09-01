using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Support.V4.View;

namespace MovilesApp.Droid
{
	[Activity (Label = "Guia", MainLauncher = true, Icon = "@drawable/ic_launcher")]

    public class MainActivity : AppCompatActivity
	{
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        SupportToolbar toolbar;

		protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            initView();
            initToolbar();
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            e.MenuItem.SetChecked(true);
            //react to click here and swap fragments or navigate


            drawerLayout.CloseDrawers();
        }

        public void initToolbar()
        {
            SetSupportActionBar(toolbar);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_black);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Subtitle = "Hola";
            SupportActionBar.Title = "Mi guia";
        }
        
        private void initView()
        {
            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.dlDrawer);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.home || item.ItemId == 16908332)
            {
                drawerLayout.OpenDrawer(GravityCompat.Start);
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}


