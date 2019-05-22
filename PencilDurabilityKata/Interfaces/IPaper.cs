using System;
using System.Collections.Generic;
using System.Text;

namespace PencilDurabilityKata.Interfaces
{
    public interface IPaper
    {
        string Text { get; }

        void AddText(string text);

        void RemoveText(string text);

        void EditText(string text);
    }
}
