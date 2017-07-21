using System;

namespace Gorgias.Business.Helper
{
    public static class CompareValue
    {
        public static int compareValues(int newParam, int oldParam)
        {
            if ((newParam == 0 && oldParam == 0) || newParam == 0)
            {
                return 0;
            }
            if (newParam != 0 && oldParam == 0)
            {
                return newParam;
            }
            else
            {
                return newParam - oldParam;
            }
        }

        public static Int64 compareValuesInt64(Int64 newParam, Int64 oldParam)
        {
            if ((newParam == 0 && oldParam == 0) || newParam == 0)
            {
                return 0;
            }
            if (newParam != 0 && oldParam == 0)
            {
                return newParam;
            }
            else
            {
                return newParam - oldParam;
            }
        }
        
    }
}