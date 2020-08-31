using System;
using System.Collections.Generic;
using System.Text;

namespace AppleStorage
{
    public class AppleProduct
    {
            public int Id { get; set; }
            public string Type { get; set; }
            public string Model { get; set; }
            public string Info { get; set; }

            public AppleProduct(string _type, string _model, string _info)
            {
                Type = _type;
                Model = _model;
                Info = _info;
            }
        
    }
}
