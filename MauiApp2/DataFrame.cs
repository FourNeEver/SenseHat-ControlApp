using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2
{
    public class OrientationData
    {
        public float roll { get; set; }
        public float pitch { get; set; }
        public float yaw { get; set; }

        public OrientationData()
        {
            pitch= 0;
            roll= 0;
            yaw= 0;
        }
    }
    public class DataFrame
    {
        public float temperatura { get; set; }
        public float pressure { get; set; }
        public float humidity { get; set; }
        public OrientationData orient { get; set; }


        public DataFrame()
        {
            temperatura = 0;
            pressure = 0;
            humidity = 0;
            orient = new OrientationData();
        }

    }

    //public class DataFrame
    //{
    //    public class Temperature
    //    {
    //        public string name { get; set; }
    //        public float value { get; set; }
    //        public string unit { get; set; }

    //        public Temperature()
    //        {
    //            name = "";
    //            value = 0;
    //            unit = "";
    //        }
    //    }

    //    public class Pressure
    //    {
    //        public string name { get; set; }
    //        public float value { get; set; }
    //        public string unit { get; set; }

    //        public Pressure()
    //        {
    //            name = "";
    //            value = 0;
    //            unit = "";
    //        }
    //    }

    //    public class Humidity
    //    {
    //        public string name { get; set; }
    //        public float value { get; set; }
    //        public string unit { get; set; }
    //        public Humidity()
    //        {
    //            name = "";
    //            value = 0;
    //            unit = "";
    //        }
    //    }

    //    public class Roll
    //    {
    //        public string name { get; set; }
    //        public float value { get; set; }
    //        public string unit { get; set; }
    //        public Roll()
    //        {
    //            name = "";
    //            value = 0;
    //            unit = "";
    //        }
    //    }

    //    public class Pitch
    //    {
    //        public string name { get; set; }
    //        public float value { get; set; }
    //        public string unit { get; set; }
    //        public Pitch()
    //        {
    //            name = "";
    //            value = 0;
    //            unit = "";
    //        }
    //    }

    //    public class Yaw
    //    {
    //        public string name { get; set; }
    //        public float value { get; set; }
    //        public string unit { get; set; }
    //        public Yaw()
    //        {
    //            name = "";
    //            value = 0;
    //            unit = "";
    //        }
    //    }

    //    public class Joy_x
    //    {
    //        public string name { get; set; }
    //        public float value { get; set; }
    //        public string unit { get; set; }
    //        public Joy_x()
    //        {
    //            name = "";
    //            value = 0;
    //            unit = "";
    //        }
    //    }

    //    public class Joy_y
    //    {
    //        public string name { get; set; }
    //        public float value { get; set; }
    //        public string unit { get; set; }
    //        public Joy_y()
    //        {
    //            name = "";
    //            value = 0;
    //            unit = "";
    //        }
    //    }

    //    public class Joy_m
    //    {
    //        public string name { get; set; }
    //        public float value { get; set; }
    //        public string unit { get; set; }
    //        public Joy_m()
    //        {
    //            name = "";
    //            value = 0;
    //            unit = "";
    //        }
    //    }

    //    public Temperature temperature { get; set; }
    //    public Pressure pressure { get; set; }
    //    public Humidity humidity { get; set; }
    //    public Roll roll { get; set; }
    //    public Pitch pitch { get; set; }
    //    public Yaw yaw { get; set; }
    //    public Joy_x joy_X { get; set; }
    //    public Joy_y joy_Y { get; set; }
    //    public Joy_m joy_M { get; set; }


    //    public DataFrame()
    //    {
    //        temperature = new Temperature();
    //        pressure = new Pressure();
    //        humidity = new Humidity();
    //        roll = new Roll();
    //        pitch = new Pitch();
    //        yaw = new Yaw();
    //        joy_X= new Joy_x();
    //        joy_Y= new Joy_y();
    //        joy_M= new Joy_m();
    //    }

    //}


    public class PixelData
    {
        public int x { get; set; }
        public int y { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public PixelData() 
        {
            x = 0;
            y = 0;
            R = 0;
            G = 0;
            B = 0;
        }


    }
}
