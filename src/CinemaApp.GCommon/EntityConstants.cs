using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.GCommon
{
    public static class EntityConstants
    {
        //Movie entity constants
        public const int MovieTitleMaxLength = 100;
        
        public const int MovieGenreMaxLength = 50;
        
        public const int MovieDirectorMaxLength = 100;
        
        public const int MovieDescriptionMaxLength = 1000;

        public const int DurationMinValue = 1;
        public const int DurationMaxValue = 300;

        public const int UrlMaxLength = 2048;
    }
}
