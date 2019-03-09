using NicoApi;

namespace NicoDataExtractor.Models {
    public class HomeViewModel {
        public string NicoUrl { get; set; }
        public string Error { get; set; }
        public string Result { get; set; }
        public NicoResponse NicoResponse { get; set; }
    }
}
