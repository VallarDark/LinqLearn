using System.Linq.Expressions;

namespace LinqLearn.Helpers
{
    public class GameFilterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _oldExpression;
        private readonly ParameterExpression _newExpression;

        public GameFilterVisitor(ParameterExpression oldExpression, ParameterExpression newExpression)
        {
            _oldExpression = oldExpression;
            _newExpression = newExpression;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (ReferenceEquals(node, _oldExpression))
            {
                return _newExpression;
            }

            return base.VisitParameter(node);
        }
    }
}
