

using System;
using System.Collections.Generic;
using System.Linq;
using Forum.Web.Models;
using HomeViewModels;
using Forum.Web.Services;
using CommunityViewModels;

namespace Services{
    
    public class Helper{


        public static string ConvertToRelativeDateTime(DateTime date)
        {
            var sec = Convert.ToInt32((DateTime.Now - date).TotalSeconds);
            var min = Convert.ToInt32((DateTime.Now - date).TotalMinutes);
            var hour = Convert.ToInt32((DateTime.Now - date).TotalHours);
            var day = Convert.ToInt32((DateTime.Now - date).TotalDays);
            
            if(min < 1)
            {
                return sec + " secs ago";
            }

            if (hour < 1)
            {
                return min + " mins ago";
            }

            if (hour < 25)
            {
                return hour + " hours ago";
            }

            return day + " days ago";

        }
    }
}