using System;
using System.Collections.Generic;
using System.Text;

namespace PencilDurabilityKata.Interfaces
{
    public interface IPaper
    {
        void AddText(string text);

        void RemoveText(string text);
    }
}
