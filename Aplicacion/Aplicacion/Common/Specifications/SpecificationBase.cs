using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Aplicacion.Common.Specifications
{
    public abstract class SpecificationBase<TEntity>
        where TEntity : class
    {
        public abstract Expression<Func<TEntity, bool>> ToExpression();

        public bool IsSatisfiedBy(TEntity entity) => ToExpression().Compile()(entity);

        public static implicit operator Expression<Func<TEntity, bool>>(SpecificationBase<TEntity> specification) =>
                specification.ToExpression();
    }
}
