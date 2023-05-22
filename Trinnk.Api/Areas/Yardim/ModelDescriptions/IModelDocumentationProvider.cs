using System;
using System.Reflection;

namespace Trinnk.Api.Areas.Yardim.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}