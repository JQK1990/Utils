using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class UtilEntry
    {
        private static UtilEntry _instance;

        private UtilEntry()
        {
            
        }
        public static UtilEntry GetInstance()
        {
            if(_instance == null)
                _instance = new UtilEntry();
            return _instance;
        }

        
    }


}
