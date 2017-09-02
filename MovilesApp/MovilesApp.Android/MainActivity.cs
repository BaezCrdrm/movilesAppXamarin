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
using SupportFragment = Android.Support.V4.App.Fragment;
using Java.Lang;

namespace MovilesApp.Droid
{
	[Activity (Label = "Guia", MainLauncher = true, Icon = "@drawable/ic_launcher")]

    public class MainActivity : AppCompatActivity
	{
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        SupportToolbar toolbar;
        SupportFragment fragment;

		protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            initView();
            initToolbar();
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            navigate(Resource.Id.nav_all);
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            e.MenuItem.SetChecked(true);
            //react to click here and swap fragments or navigate
            navigate(e.MenuItem.ItemId);

            drawerLayout.CloseDrawers();
        }

        public void initToolbar()
        {
            SetSupportActionBar(toolbar);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_black);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = GetString(Resource.String.app_name);
        }
        
        private void initView()
        {
            toolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.dlDrawer);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
        }

        private bool navigate(int id)
        {
            fragment = null;
            fragment = new AllEvents();
            Bundle args = new Bundle();
            long newNav = 0;
            string title = GetString(Resource.String.app_name);

            switch (id)
            {
                case Resource.Id.nav_all:
                    title = GetString(Resource.String.menu_all);
                    newNav = -1;
                    break;

                case Resource.Id.nav_football:
                    title = GetString(Resource.String.menu_football);
                    newNav = 1;
                    break;

                case Resource.Id.nav_soccer:
                    title = GetString(Resource.String.menu_soccer);
                    newNav = 2;
                    break;

                case Resource.Id.nav_basketball:
                    title = GetString(Resource.String.menu_basketball);
                    newNav = 3;
                    break;

                case Resource.Id.nav_baseball:
                    title = GetString(Resource.String.menu_baseball);
                    newNav = 4;
                    break;

                case Resource.Id.nav_music:
                    title = GetString(Resource.String.menu_music);
                    newNav = 5;
                    break;

                case Resource.Id.nav_awards:
                    title = GetString(Resource.String.menu_awards);
                    newNav = 6;
                    break;

                case Resource.Id.nav_other:
                    title = GetString(Resource.String.menu_other);
                    newNav = 7;
                    break;

                //case Resource.Id.nav_box:
                //title = GetString(Resource.String.menu_box);
                //    newNav = 8;
                //    break;
            }
            SupportActionBar.Title = title;
            Long serializable = (Long)newNav;
            args.PutSerializable("EventType", serializable);
            //args.PutSerializable("Region", '');
            fragment.Arguments = args;
            navigate(Resource.Id.rLayoutEventList, fragment);

            return true;
        }

        private void navigate(int replaced, SupportFragment frag)
        {
            RelativeLayout rl = FindViewById<RelativeLayout>(Resource.Id.rLayoutEventDetails);
            if (replaced == Resource.Id.rLayoutEventList)
                rl.Visibility = ViewStates.Gone;
            else
                rl.Visibility = ViewStates.Visible;

            SupportFragmentManager.BeginTransaction().
                Replace(replaced, frag).Commit();
            drawerLayout.CloseDrawer(GravityCompat.Start);

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


