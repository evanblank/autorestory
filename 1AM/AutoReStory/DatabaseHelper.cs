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
using Android.Database.Sqlite;
using Android.Database;
using System.Collections;

namespace AutoReStory
{
    public class VehicleDbHelper: SQLiteOpenHelper

    {
        private const string APP_DATABASENAME = "AutoRestory.db3";
        private const int APP_DATABASE_VERSION = 1;

        public VehicleDbHelper(Context ctx):
            base(ctx, APP_DATABASENAME, null, APP_DATABASE_VERSION)
        {

        }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(@"CREATE TABLE IF NOT EXISTS Vehicle(
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Year TEXT NOT NULL,
                            Make  TEXT NOT NULL,
                            Model   TEXT NULL,
                            Color TEXT,
                            Description TEXT,
                            PercentagePaint INTEGER NOT NULL,
                            PercentageBody INTEGER NOT NULL,
                            PercentageGlass INTEGER NOT NULL,
                            PercentageTrim INTEGER NOT NULL,
                            PercentageUpholstery INTEGER NOT NULL,
                            PercentageMechanical INTEGER NOT NULL,
                            PercentageElectrical INTEGER NOT NULL,
                            PercentageFrame INTEGER NOT NULL,
                            PercentageTires INTEGER NOT NULL,
                            PercentageOverall INTEGER NOT NULL
                            )");

        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXISTS Vehicle");
            OnCreate(db);
        }

        //Retrive All Vehicle Details
        public IList<Vehicle> GetAllAutos()
        {

            SQLiteDatabase db = this.ReadableDatabase;

            ICursor c = db.Query("Vehicle", new string[] { "Id", "Year", "Make", "Model", "Color", "Description","PercentagePaint","PercentageBody","PercentageGlass","PercentageTrim","PercentageUpholstery","PercentageMechanical","PercentageElectrical","PercentageFrame","PercentageTires","PercentageOverall" }, null, null, null, null, null);

            var autos = new List<Vehicle>();

            while (c.MoveToNext())
            {
                autos.Add(new Vehicle
                {
                    Id = c.GetInt(0),
                    Year = c.GetString(1),
                    Make = c.GetString(2),
                    Model = c.GetString(3),
                    Color = c.GetString(4),
                    Description = c.GetString(5),
                    PercentagePaint = c.GetInt(6),
                    PercentageBody = c.GetInt(7),
                    PercentageGlass = c.GetInt(8),
                    PercentageTrim = c.GetInt(9),
                    PercentageUpholstery = c.GetInt(10),
                    PercentageMechanical = c.GetInt(11),
                    PercentageElectrical = c.GetInt(12),
                    PercentageFrame = c.GetInt(13),
                    PercentageTires = c.GetInt(14),
                    PercentageOverall = c.GetInt(15)
                });
            }

            c.Close();
            db.Close();

            return autos;
        }


        //Retrive All Contact Details
        public IList<Vehicle> GetAutosBySearchName(string nameToSearch)
        {

            SQLiteDatabase db = this.ReadableDatabase;

            ICursor c = db.Query("Vehicle", new string[] {"Id", "Year", "Make", "Model", "Color", "Description", "PercentagePaint", "PercentageBody", "PercentageGlass", "PercentageTrim", "PercentageUpholstery", "PercentageMechanical", "PercentageElectrical", "PercentageFrame", "PercentageTires", "PercentageOverall" }, "Model LIKE ?", new string[] { "%" + nameToSearch.ToUpper() + "%" }, null, null, null, null);

            var autos = new List<Vehicle>();

            while (c.MoveToNext())
            {
                autos.Add(new Vehicle
                {
                    Id = c.GetInt(0),
                    Year = c.GetString(1),
                    Make = c.GetString(2),
                    Model = c.GetString(3),
                    Color = c.GetString(4),
                    Description = c.GetString(5),
                    PercentagePaint = c.GetInt(6),
                    PercentageBody = c.GetInt(7),
                    PercentageGlass = c.GetInt(8),
                    PercentageTrim = c.GetInt(9),
                    PercentageUpholstery = c.GetInt(10),
                    PercentageMechanical = c.GetInt(11),
                    PercentageElectrical = c.GetInt(12),
                    PercentageFrame = c.GetInt(13),
                    PercentageTires = c.GetInt(14),
                    PercentageOverall = c.GetInt(15)
                });
            }

            c.Close();
            db.Close();

            return autos;
        }

        //Add New Auto
        public void AddNewAuto (Vehicle autoinfo)
        {
            SQLiteDatabase db = this.WritableDatabase;
            ContentValues vals = new ContentValues();
            vals.Put("Year", autoinfo.Year);
            vals.Put("Make", autoinfo.Make);
            vals.Put("Model", autoinfo.Model);
            vals.Put("Color", autoinfo.Color);
            vals.Put("Description", autoinfo.Description);
            vals.Put("PercentagePaint", autoinfo.PercentagePaint);
            vals.Put("PercentageBody", autoinfo.PercentageBody);
            vals.Put("PercentageGlass", autoinfo.PercentageGlass);
            vals.Put("PercentageTrim", autoinfo.PercentageTrim);
            vals.Put("PercentageUpholstery", autoinfo.PercentageUpholstery);
            vals.Put("PercentageMechanical", autoinfo.PercentageMechanical);
            vals.Put("PercentageElectrical", autoinfo.PercentageElectrical);
            vals.Put("PercentageFrame", autoinfo.PercentageFrame);
            vals.Put("PercentageTires", autoinfo.PercentageTires);
            vals.Put("PercentageOverall", autoinfo.PercentageOverall);

            db.Insert("Vehicle", null, vals);
        }

        //Get auto details by auto Id
        public ICursor getAutoById(int id)
        {
            SQLiteDatabase db = this.ReadableDatabase;
            ICursor res = db.RawQuery("select * from Vehicle where Id=" + id + "", null);
            return res;
        }

        //Update Existing auto
        public void UpdateAuto (Vehicle contitem)
        {
            if (contitem == null)
            {
                return;
            }

            //Obtain writable database
            SQLiteDatabase db = this.WritableDatabase;

            //Prepare content values
            ContentValues vals = new ContentValues();
            vals.Put("Year", contitem.Year);
            vals.Put("Make", contitem.Make);
            vals.Put("Model", contitem.Model);
            vals.Put("Color", contitem.Color);
            vals.Put("Description", contitem.Description);
            vals.Put("PercentagePaint", contitem.PercentagePaint);
            vals.Put("PercentageBody", contitem.PercentageBody);
            vals.Put("PercentageGlass", contitem.PercentageGlass);
            vals.Put("PercentageTrim", contitem.PercentageTrim);
            vals.Put("PercentageUpholstery", contitem.PercentageUpholstery);
            vals.Put("PercentageMechanical", contitem.PercentageMechanical);
            vals.Put("PercentageElectrical", contitem.PercentageElectrical);
            vals.Put("PercentageFrame", contitem.PercentageFrame);
            vals.Put("PercentageTires", contitem.PercentageTires);
            vals.Put("PercentageOverall", contitem.PercentageOverall);


            ICursor cursor = db.Query("Vehicle",
                    new String[] {"Id", "Year", "Make", "Model", "Color", "Description", "PercentagePaint", "PercentageBody", "PercentageGlass", "PercentageTrim", "PercentageUpholstery", "PercentageMechanical", "PercentageElectrical", "PercentageFrame", "PercentageTires", "PercentageOverall" }, "Id=?", new string[] { contitem.Id.ToString() }, null, null, null, null);

            if (cursor != null)
            {
                if (cursor.MoveToFirst())
                {
                    // update the row
                    db.Update("Vehicle", vals, "Id=?", new String[] { cursor.GetString(0) });
                }

                cursor.Close();
            }

        }


        //Delete Existing auto
        public void DeleteAuto(string autoId)
        {
            if (autoId == null)
            {
                return;
            }

            //Obtain writable database
            SQLiteDatabase db = this.WritableDatabase;

            ICursor cursor = db.Query("Vehicle",
                    new String[] { "Id", "Year", "Make", "Model", "Color", "Description", "PercentagePaint", "PercentageBody", "PercentageGlass", "PercentageTrim", "PercentageUpholstery", "PercentageMechanical", "PercentageElectrical", "PercentageFrame", "PercentageTires", "PercentageOverall" }, "Id=?", new string[] { autoId }, null, null, null, null);

            if (cursor != null)
            {
                if (cursor.MoveToFirst())
                {
                    // update the row
                    db.Delete("Vehicle", "Id=?", new String[] { cursor.GetString(0) });
                }

                cursor.Close();
            }

        }
    }
}