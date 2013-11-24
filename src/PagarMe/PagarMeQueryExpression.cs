#region License

// The MIT License (MIT)
// 
// Copyright (c) 2013 Pagar.me
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using PagarMe.Serializer;
using Remotion.Linq.Clauses.Expressions;
using Remotion.Linq.Clauses.ExpressionTreeVisitors;
using Remotion.Linq.Parsing;

namespace PagarMe
{
    internal class PagarMeQueryExpression : ThrowingExpressionTreeVisitor
    {
        private readonly StringBuilder _builder;
        private readonly List<Tuple<string, string>> _expressions;
        private bool _isLeft;
        private MemberInfo _left;

        public PagarMeQueryExpression()
        {
            _expressions = new List<Tuple<string, string>>();
            _builder = new StringBuilder();
        }

        public IEnumerable<Tuple<string, string>> Expressions
        {
            get { return _expressions; }
        }

        protected override Expression VisitQuerySourceReferenceExpression(QuerySourceReferenceExpression expression)
        {
            return expression;
        }

        protected override Expression VisitUnaryExpression(UnaryExpression expression)
        {
            if (expression.NodeType == ExpressionType.Convert)
            {
                VisitExpression(expression.Operand);
                return expression;
            }

            return base.VisitUnaryExpression(expression);
        }

        protected override Expression VisitBinaryExpression(BinaryExpression expression)
        {
            string left;

            _isLeft = true;
            VisitExpression(expression.Left);
            _isLeft = false;

            left = _builder.ToString();
            _builder.Clear();

            switch (expression.NodeType)
            {
                case ExpressionType.Equal:
                    break;

                case ExpressionType.GreaterThan:
                    _builder.Append(">");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    _builder.Append(">=");
                    break;
                case ExpressionType.LessThan:
                    _builder.Append("<");
                    break;
                case ExpressionType.LessThanOrEqual:
                    _builder.Append("<=");
                    break;

                case ExpressionType.IsTrue:
                    _builder.Append("1");
                    break;
                case ExpressionType.IsFalse:
                    _builder.Append("0");
                    break;

                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    break;

                default:
                    return base.VisitBinaryExpression(expression);
            }

            VisitExpression(expression.Right);

            _expressions.Add(new Tuple<string, string>(left, _builder.ToString()));
            _builder.Clear();
            _left = null;

            return expression;
        }

        protected override Expression VisitMemberExpression(MemberExpression expression)
        {
            JsonPropertyAttribute property = expression.Member.GetCustomAttribute<JsonPropertyAttribute>();
            string name = property != null && property.PropertyName != null
                ? property.PropertyName
                : expression.Member.Name;

            VisitExpression(expression.Expression);

            if (expression.Expression is MemberExpression)
            {
                _builder.AppendFormat("[{0}]", name);
            }
            else
            {
                _builder.Append(name);
            }

            if (_isLeft)
                _left = expression.Member;

            return expression;
        }

        protected override Expression VisitConstantExpression(ConstantExpression expression)
        {
            string value = UrlSerializer.ConvertValue(_left as PropertyInfo, expression.Value);

            if (value == null)
                return base.VisitConstantExpression(expression);

            _builder.Append(value);

            return expression;
        }

        private static string FormatUnhandledItem<T>(T unhandledItem)
        {
            var itemAsExpression = unhandledItem as Expression;
            return itemAsExpression != null
                ? FormattingExpressionTreeVisitor.Format(itemAsExpression)
                : unhandledItem.ToString();
        }

        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
        {
            return
                new NotSupportedException(
                    string.Format("The expression '{0}' (type: {1}) is not supported by this LINQ provider.",
                        FormatUnhandledItem(unhandledItem), typeof(T)));
        }
    }
}