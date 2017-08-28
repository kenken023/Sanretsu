using System;
using System.Collections.Generic;
using System.Text;

namespace Sanretsu.Dependencies
{
    public interface ICopyToClipboard
    {
        void Copy(string text);
    }
}
