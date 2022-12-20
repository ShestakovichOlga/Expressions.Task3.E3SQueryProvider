﻿/*
 * This task is a bit harder than the previous two.
 * Feel free to change the E3SLinqProvider and any other classes if needed.
 * Possibly, after these changes you will need to rewrite existing tests to make them work again =).
 *
 * The task: implement support of && operator for IQueryable. The final request generated by FTSRequestGenerator, should
 * imply the following rules: https://kb.epam.com/display/EPME3SDEV/Telescope+public+REST+for+data#TelescopepublicRESTfordata-FTSRequestSyntax
 */

using System;
using System.Linq;
using System.Linq.Expressions;
using Expressions.Task3.E3SQueryProvider.Models.Entities;
using Xunit;

namespace Expressions.Task3.E3SQueryProvider.Test
{
    public class E3SAndOperatorSupportTests
    {
        #region SubTask 3: AND operator support

        [Fact]
        public void TestAndQueryable()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<IQueryable<EmployeeEntity>, IQueryable<EmployeeEntity>>> expression
                = query => query.Where(e => e.Workstation == "EPRUIZHW006" && e.Manager.StartsWith("John"));
            /*
             * The expression above should be converted to the following FTSQueryRequest and then serialized inside FTSRequestGenerator:
             * "statements": [
                { "query":"Workstation:(EPRUIZHW006)"},
                { "query":"Manager:(John*)"}
                // Operator between queries is AND, in other words result set will fit to both statements above
              ],
             */

            // todo: create asserts for this test by yourself, because they will depend on your final implementation
            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(EPRUIZHW006) AND Manager:(John*)", translated);
        }

            #endregion
    }
}
