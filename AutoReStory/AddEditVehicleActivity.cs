using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Database;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Provider;

using Java.IO;

using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace AutoReStory
{
    [Activity(Label = "Add/Edit Autos")]
    public class AddEditVehicleActivity : Activity
    {
        private File _dir;
        private File _file;

        EditText txtId,
                    txtYear,
                    txtMake,
                    txtModel,
                    txtColor,
                    txtDescription;

        TextView tvPaint,
                    tvBody,
                    tvGlass,
                    tvTrim,
                    tvUpholstery,
                    tvMechanical,
                    tvElectrical,
                    tvFrame,
                    tvTires,
                    tvOverall;

        SeekBar sbPaint,
                    sbBody,
                    sbGlass,
                    sbTrim,
                    sbUpholstery,
                    sbMechanical,
                    sbElectrical,
                    sbFrame,
                    sbTires,
                    sbOverall;

        Button      btnSave;

        ImageView imageView;

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
            int height = imageView.Height;
            int width = Resources.DisplayMetrics.WidthPixels;
            using (Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height))
            {
                imageView.RecycleBitmap();
                imageView.SetImageBitmap(bitmap);
            }
        }




        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddEditAutos);

            txtId = FindViewById<EditText>(Resource.Id.addEdit_AutoId);
            txtYear = FindViewById<EditText>(Resource.Id.addEdit_Year);
            txtMake = FindViewById<EditText>(Resource.Id.addEdit_Make);
            txtModel = FindViewById<EditText>(Resource.Id.addEdit_Model);
            txtColor = FindViewById<EditText>(Resource.Id.addEdit_Color);
            txtDescription = FindViewById<EditText>(Resource.Id.addEdit_Description);

            btnSave = FindViewById<Button>(Resource.Id.addEdit_btnSave);

            tvPaint = FindViewById<TextView>(Resource.Id.paint);
            tvBody = FindViewById<TextView>(Resource.Id.body);
            tvGlass = FindViewById<TextView>(Resource.Id.glass);
            tvTrim = FindViewById<TextView>(Resource.Id.trim);
            tvUpholstery = FindViewById<TextView>(Resource.Id.upholstery);
            tvMechanical = FindViewById<TextView>(Resource.Id.mechanical);
            tvElectrical = FindViewById<TextView>(Resource.Id.electrical);
            tvFrame = FindViewById<TextView>(Resource.Id.frame);
            tvTires = FindViewById<TextView>(Resource.Id.tires);
            tvOverall = FindViewById<TextView>(Resource.Id.overall);

            sbPaint = FindViewById<SeekBar>(Resource.Id.paintBar);
            sbBody = FindViewById<SeekBar>(Resource.Id.bodyBar);
            sbGlass = FindViewById<SeekBar>(Resource.Id.glassBar);
            sbTrim = FindViewById<SeekBar>(Resource.Id.trimBar);
            sbUpholstery = FindViewById<SeekBar>(Resource.Id.upholsteryBar);
            sbMechanical = FindViewById<SeekBar>(Resource.Id.mechanicalBar);
            sbElectrical = FindViewById<SeekBar>(Resource.Id.electricalBar);
            sbFrame = FindViewById<SeekBar>(Resource.Id.frameBar);
            sbTires = FindViewById<SeekBar>(Resource.Id.tiresBar);
            sbOverall = FindViewById<SeekBar>(Resource.Id.overallBar);

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                Button cameraButton = FindViewById<Button>(Resource.Id.openCamera);
                imageView = FindViewById<ImageView>(Resource.Id.imageView1);

                cameraButton.Click += TakeAPicture;
            }

            btnSave.Click += buttonSave_Click;

            string editId = Intent.GetStringExtra("AutoId") ?? string.Empty;

            if (editId.Trim().Length > 0)
            {
                txtId.Text = editId;
                LoadDataForEdit(editId);
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

        private void LoadDataForEdit(string autoId)
        {
            VehicleDbHelper db = new VehicleDbHelper(this);
            ICursor cData = db.getAutoById(int.Parse(autoId));
            if (cData.MoveToFirst())
            {
                txtYear.Text = cData.GetString(cData.GetColumnIndex("Year"));
                txtMake.Text = cData.GetString(cData.GetColumnIndex("Make"));
                txtModel.Text = cData.GetString(cData.GetColumnIndex("Model"));
                txtColor.Text = cData.GetString(cData.GetColumnIndex("Color"));
                txtDescription.Text = cData.GetString(cData.GetColumnIndex("Description"));

                sbPaint.Progress = cData.GetInt(cData.GetColumnIndex("PercentagePaint"));
                sbBody.Progress = cData.GetInt(cData.GetColumnIndex("PercentageBody"));
                sbGlass.Progress = cData.GetInt(cData.GetColumnIndex("PercentageGlass"));
                sbTrim.Progress = cData.GetInt(cData.GetColumnIndex("PercentageTrim"));
                sbUpholstery.Progress = cData.GetInt(cData.GetColumnIndex("PercentageUpholstery"));
                sbMechanical.Progress = cData.GetInt(cData.GetColumnIndex("PercentageMechanical"));
                sbElectrical.Progress = cData.GetInt(cData.GetColumnIndex("PercentageElectrical"));
                sbFrame.Progress = cData.GetInt(cData.GetColumnIndex("PercentageFrame"));
                sbTires.Progress = cData.GetInt(cData.GetColumnIndex("PercentageTires"));
                sbOverall.Progress = cData.GetInt(cData.GetColumnIndex("PercentageOverall"));

            }
        }

        void buttonSave_Click(object sender, EventArgs e)
        {
            VehicleDbHelper db = new VehicleDbHelper(this);
            if (txtYear.Text.Trim().Length < 1)
            {
                Toast.MakeText(this, "Enter Year.", ToastLength.Short).Show();
                return;
            }

            if (txtMake.Text.Trim().Length < 1)
            {
                Toast.MakeText(this, "Enter Make.", ToastLength.Short).Show();
                return;
            }

            if (txtModel.Text.Trim().Length < 1)
            {
                Toast.MakeText(this, "Enter Model.", ToastLength.Short).Show();
                return;
            }

            if (txtColor.Text.Trim().Length < 1)
            {
                Toast.MakeText(this, "Enter Color.", ToastLength.Short).Show();
                return;
            }

            Vehicle ab = new Vehicle();

            if (txtId.Text.Trim().Length > 0)
            {
                ab.Id = int.Parse(txtId.Text);
            }
            ab.Year = txtYear.Text;
            ab.Make = txtMake.Text;
            ab.Model = txtModel.Text;
            ab.Color = txtColor.Text;
            ab.Description = txtDescription.Text;

            if (sbPaint.Progress > 0)
            {
                ab.PercentagePaint = sbPaint.Progress;
            }
            //Body
            if (sbBody.Progress > 0)
            {
                ab.PercentageBody = sbBody.Progress;
            }
            //Glass
            if (sbGlass.Progress > 0)
            {
                ab.PercentageGlass = sbGlass.Progress;
            }
            //Trim
            if (sbTrim.Progress > 0)
            {
                ab.PercentageTrim = sbTrim.Progress;
            }
            //Upholstery
            if (sbUpholstery.Progress > 0)
            {
                ab.PercentageUpholstery = sbUpholstery.Progress;
            }
            //Mechanical
            if (sbMechanical.Progress > 0)
            {
                ab.PercentageMechanical = sbMechanical.Progress;
            }
            //Electrical
            if (sbMechanical.Progress > 0)
            {
                ab.PercentageElectrical = sbMechanical.Progress;
            }
            //Frame
            if (sbFrame.Progress > 0)
            {
                ab.PercentageFrame = sbFrame.Progress;
            }
            //Tires
            if (sbTires.Progress > 0)
            {
                ab.PercentageTires = sbTires.Progress;
            }
            if (sbOverall.Progress > 0)
            {
                ab.PercentageOverall = sbOverall.Progress;
            }
           

            try
            {

                if (txtId.Text.Trim().Length > 0)
                {
                    db.UpdateAuto(ab);
                    Toast.MakeText(this, "Auto Updated Successfully.", ToastLength.Short).Show();
                }
                else
                {
                    db.AddNewAuto(ab);
                    Toast.MakeText(this, "New Auto Created Successfully.", ToastLength.Short).Show();
                }

                Finish();

                //Go to main activity after save/edit
                var mainActivity = new Intent(this, typeof(MainActivity));
                StartActivity(mainActivity);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}