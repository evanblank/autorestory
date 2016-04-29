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

namespace AutoReStory
{
    [Activity(Label = "AutoListBaseAdapter")]
    public partial class AutoListBaseAdapter : BaseAdapter<Vehicle>
    {
        IList<Vehicle> autoListArrayList;
        LayoutInflater mInflater;
        Context activity;
        public AutoListBaseAdapter(Context context,
                                                IList<Vehicle> results)
        {
            this.activity = context;
            autoListArrayList = results;
            mInflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
        }

        public override int Count
        {
            get { return autoListArrayList.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Vehicle this[int position]
        {
            get { return autoListArrayList[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView btnDelete;
            AutosViewHolder holder = null;
            if (convertView == null)
            {
                convertView = mInflater.Inflate(Resource.Layout.list_row_auto_list, null);
                holder = new AutosViewHolder();

                holder.txtYear = convertView.FindViewById<TextView>(Resource.Id.lr_year);
                holder.txtMake = convertView.FindViewById<TextView>(Resource.Id.lr_make);
                holder.txtModel = convertView.FindViewById<TextView>(Resource.Id.lr_model);
                holder.txtColor = convertView.FindViewById<TextView>(Resource.Id.lr_color);
                holder.txtDescription = convertView.FindViewById<TextView>(Resource.Id.lr_description);
                
                btnDelete = convertView.FindViewById<ImageView>(Resource.Id.lr_deleteBtn);


                btnDelete.Click += (object sender, EventArgs e) =>
                {

                    AlertDialog.Builder builder = new AlertDialog.Builder(activity);
                    AlertDialog confirm = builder.Create();
                    confirm.SetTitle("Confirm Delete");
                    confirm.SetMessage("Are you sure delete?");
                    confirm.SetButton("OK", (s, ev) =>
                    {
                        var poldel = (int)((sender as ImageView).Tag);

                        string id = autoListArrayList[poldel].Id.ToString();
                        string model = autoListArrayList[poldel].Model;

                        autoListArrayList.RemoveAt(poldel);

                        DeleteSelectedAuto(id);
                        NotifyDataSetChanged();

                        Toast.MakeText(activity, "Auto Deleted Successfully", ToastLength.Short).Show();
                    });
                    confirm.SetButton2("Cancel", (s, ev) =>
                    {

                    });

                    confirm.Show();
                };

                convertView.Tag = holder;
                btnDelete.Tag = position;
            }
            else
            {
                btnDelete = convertView.FindViewById<ImageView>(Resource.Id.lr_deleteBtn);
                holder = convertView.Tag as AutosViewHolder;
                btnDelete.Tag = position;
            }

            holder.txtYear.Text = autoListArrayList[position].Year.ToString();
            holder.txtMake.Text = autoListArrayList[position].Make;
            holder.txtModel.Text = autoListArrayList[position].Model;
            holder.txtColor.Text = autoListArrayList[position].Color;
            holder.txtDescription.Text = autoListArrayList[position].Description;
            /*
            holder.txtPercentagePaint.Text = autoListArrayList[position].PercentagePaint;
            holder.txtPercentageBody.Text = autoListArrayList[position].PercentageBody;
            holder.txtPercentageGlass.Text = autoListArrayList[position].PercentageGlass;
            holder.txtPercentageTrim.Text = autoListArrayList[position].PercentageTrim;
            holder.txtPercentageUpholstery.Text = autoListArrayList[position].PercentageUpholstery;
            holder.txtPercentageMechanical.Text = autoListArrayList[position].PercentageMechanical;
            holder.txtPercentageElectrical.Text = autoListArrayList[position].PercentageElectrical;
            holder.txtPercentageFrame.Text = autoListArrayList[position].PercentageFrame;
            holder.txtPercentageTires.Text = autoListArrayList[position].PercentageTires;
            holder.txtPercentageOverall.Text = autoListArrayList[position].PercentageOverall;
            */

            if (position % 2 == 0)
            {
                convertView.SetBackgroundResource(Resource.Drawable.list_selector);
            }
            else
            {
                convertView.SetBackgroundResource(Resource.Drawable.list_selector_alternate);
            }

            return convertView;
        }

        public IList<Vehicle> GetAllData()
        {
            return autoListArrayList;
        }

        public class AutosViewHolder : Java.Lang.Object
        {
            public TextView txtYear { get; set; }
            public TextView txtMake { get; set; }
            public TextView txtModel { get; set; }
            public TextView txtColor { get; set; }
            public TextView txtDescription { get; set; }
            public TextView txtPercentagePaint { get; set; }
            public TextView txtPercentageBody { get; set; }
            public TextView txtPercentageGlass { get; set; }
            public TextView txtPercentageTrim { get; set; }
            public TextView txtPercentageUpholstery { get; set; }
            public TextView txtPercentageMechanical { get; set; }
            public TextView txtPercentageElectrical { get; set; }
            public TextView txtPercentageFrame { get; set; }
            public TextView txtPercentageTires { get; set; }
            public TextView txtPercentageOverall { get; set; }
        }

        private void DeleteSelectedAuto(string autoId)
        {
            VehicleDbHelper _db = new VehicleDbHelper(activity);
            _db.DeleteAuto(autoId);
        }

    }
}