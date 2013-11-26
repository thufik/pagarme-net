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
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;

namespace PagarMe
{
    internal class PagarMeModelQuery<T> : QueryModelVisitorBase
    {
        private readonly PagarMeModelDefinition _model;
        private readonly PagarMeProvider _provider;
        private readonly PagarMeQuery _query;

        public PagarMeModelQuery(PagarMeModelDefinition model, PagarMeProvider pagarme)
        {
            _model = model;
            _provider = pagarme;
            _query = new PagarMeQuery(pagarme, "GET", _model.Endpoint);
        }

        public IEnumerable<T> Execute()
        {
            PagarMeQueryResponse response = _query.Execute();

            response.Validate();

            return from JObject obj in JArray.Parse(response.Data) select (T)_model.Build(obj, _provider);
        }

        public override void VisitResultOperator(ResultOperatorBase resultOperator, QueryModel queryModel, int index)
        {
            if (resultOperator is FirstResultOperator)
            {
                _query.Take = 1;
                return;
            }

            var takeResult = resultOperator as TakeResultOperator;
            if (takeResult != null)
            {
                var exp = takeResult.Count;

                if (exp.NodeType == ExpressionType.Constant)
                {
                    _query.Take = (int)((ConstantExpression)exp).Value;
                }
                else
                {
                    throw new NotSupportedException(
                        "Currently not supporting methods or variables in the Take clause.");
                }

                return;
            }

            base.VisitResultOperator(resultOperator, queryModel, index);
        }

        public override void VisitQueryModel(QueryModel queryModel)
        {
            queryModel.SelectClause.Accept(this, queryModel);
            queryModel.MainFromClause.Accept(this, queryModel);

            VisitBodyClauses(queryModel.BodyClauses, queryModel);
            VisitResultOperators(queryModel.ResultOperators, queryModel);
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            PagarMeQueryExpression exp = new PagarMeQueryExpression();
            exp.VisitExpression(whereClause.Predicate);

            foreach (var clause in exp.Expressions)
                _query.AddQuery(clause.Item1, clause.Item2);

            base.VisitWhereClause(whereClause, queryModel, index);
        }
    }
}