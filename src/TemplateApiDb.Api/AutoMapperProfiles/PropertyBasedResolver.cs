using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using System.Linq.Expressions;
using System.Reflection;

namespace TemplateApiDb.Api.AutoMapperProfiles
{
    public class PropertyBasedResolver : IValueResolver
    {
        public PropertyInfo Property { get; set; }

        public Type ResolvedType => throw new NotImplementedException();

        public Expression GetExpression(IGlobalConfiguration configuration, MemberMap memberMap, Expression source, Expression destination, Expression destinationMember)
        {
            throw new NotImplementedException();
        }

        public MemberInfo GetSourceMember(MemberMap memberMap)
        {
            throw new NotImplementedException();
        }
    }
}
