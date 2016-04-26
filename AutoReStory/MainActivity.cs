using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Provider;

using Java.IO;

using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace AutoReStory
{
    [Activity(Label = "AutoReStory", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button btnAdd, btnSearch;
        EditText txtSearch;
        ListView lv;
        IList<AddressBook> listItsms = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            btnAdd = FindViewById<Button> (Resource.Id.contactList_btnAdd);
            btnSearch = FindViewById<Button>(Resource.Id.contactList_btnSearch);
            txtSearch = FindViewById<EditText>(Resource.Id.contactList_txtSearch);
            lv = FindViewById<ListView>(Resource.Id.contactList_listView);

            btnAdd.Click += delegate
            {
                var activityAddEdit = new Intent(this, typeof(AddEditVehicleActivity));
                StartActivity(activityAddEdit);
            };

            btnSearch.Click += delegate
            {
                LoadAutosInList();
            };

            LoadAutosInList();

        }

        private void LoadAutosInList()
        {
            VehicleDbHelper dbVals = new VehicleDbHelper(this);
            if (txtSearch.Text.Trim().Length < 1)
            {
                listItsms = dbVals.GetAllAutos();
            }
            else
            {

                listItsms = dbVals.GetAutosBySearchName(txtSearch.Text.Trim());
            }


            lv.Adapter = new AutoListBaseAdapter(this, listItsms);

            lv.ItemLongClick += lv_ItemLongClick;
        }

        private void lv_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            AddressBook o = listItsms[e.Position];

            //  Toast.MakeText(this, o.Id.ToString(), ToastLength.Long).Show();

            var activityAddEdit = new Intent(this, typeof(AddEditVehicleActivity));
            activityAddEdit.PutExtra("AutoId", o.Id.ToString());
            activityAddEdit.PutExtra("Year", o.Year);
            StartActivity(activityAddEdit);
        }



        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

		}


    }
}

