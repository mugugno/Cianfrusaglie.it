using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cianfrusaglie.Constants;
using Microsoft.AspNet.Http;

namespace Cianfrusaglie.ViewModels.Preference
{
    public class PreferenceViewModel
    {
        public Dictionary<int, bool> CategoryDictionary { get; set; }
    }
}
