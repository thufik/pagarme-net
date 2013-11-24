using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

            if (response.Status != 200)
                throw new PagarMeException(response);

            return from JObject obj in JArray.Parse(response.Data) select (T)_model.Build(obj, _provider);
        }

        public override void VisitResultOperator(ResultOperatorBase resultOperator, QueryModel queryModel, int index)
        {
            if (resultOperator is FirstResultOperator)
            {
                _query.Take = 1;
                return;
            }

            if (resultOperator is TakeResultOperator)
            {
                var exp = ((TakeResultOperator)resultOperator).Count;

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
