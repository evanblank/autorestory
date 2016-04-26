using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Provider;
using Android.Widget;

using Java.IO;

using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace AutoReStory
{
    [Activity(Label = "AutoReStory", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
		private File _dir;
		private File _file;
		private ImageView _imageView;
        private Button btnAdd, btnSearch;
        private EditText txtSearch;
        private ListView lv;
        private IList<Vehicle> listItsms = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            btnAdd = FindViewById<Button>(Resource.Id.autoList_btnAdd);
            btnSearch = FindViewById<Button>(Resource.Id.autoList_btnSearch);
            txtSearch = FindViewById<EditText>(Resource.Id.autoList_txtSearch);
            lv = FindViewById<ListView>(Resource.Id.autoList_listView);


            if (IsThereAnAppToTakePictures())
			{
				CreateDirectoryForPictures();

				Button cameraButton = FindViewById<Button>(Resource.Id.openCamera);
				_imageView = FindViewById<ImageView>(Resource.Id.imageView1);

				cameraButton.Click += TakeAPicture;
			}

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
            Vehicle o = listItsms[e.Position];

            //  Toast.MakeText(this, o.Id.ToString(), ToastLength.Long).Show();

            var activityAddEdit = new Intent(this, typeof(AddEditVehicleActivity));
            activityAddEdit.PutExtra("AutoId", o.Id.ToString());
            activityAddEdit.PutExtra("Year", o.Year);
            StartActivity(activityAddEdit);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			// make it available in the gallery
			Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
			Uri contentUri = Uri.FromFile(_file);
			mediaScanIntent.SetData(contentUri);
			SendBroadcast(mediaScanIntent);

			// display in ImageView. We will resize the bitmap to fit the display
			// Loading the full sized image will consume to much memory 
			// and cause the application to crash.
			int height = _imageView.Height;
			int width = Resources.DisplayMetrics.WidthPixels;
			using (Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height))
			{
				_imageView.RecycleBitmap ();
				_imageView.SetImageBitmap(bitmap);
			}
		}

		private void CreateDirectoryForPictures()
		{
			_dir = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "AutoReStory");
			if (!_dir.Exists())
			{
				_dir.Mkdirs();
			}
		}

		private bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		private void TakeAPicture(object sender, EventArgs eventArgs)
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);

			_file = new File(_dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

			intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(_file));

			StartActivityForResult(intent, 0);
		}
    }
}

