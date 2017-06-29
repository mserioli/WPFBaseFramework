using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Model
{
    public class Locale
    {
        private string _localeName;

        public string LocaleName {
            get { return _localeName; }
            set { _localeName = value; }
        }            

        private string _localeCode;

        public string LocaleCode
        {
            get { return _localeCode; }
            set { _localeCode = value; }
        }

        public override string ToString()
        {
            return _localeName;
        }
    }
}
