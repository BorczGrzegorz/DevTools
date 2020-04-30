using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.JiraApi.Builders
{
    internal static class BuildersExtensions
    {
        internal static JqlBuilder If(this JqlBuilder builder, bool condition, Action<JqlBuilder> action)
        {
            if (condition)
            {
                action(builder);    
            }

            return builder;
        }
    }
}
