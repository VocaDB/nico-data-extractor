using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicoDataExtractor.Models {
    public class HomeViewModel {
        public string NicoUrl { get; set; }
        public string Error { get; set; }
        public string Result { get; set; }
        public NicoResponse NicoResponse { get; set; }
    }
}
