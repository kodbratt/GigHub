using NUnit.Framework;
using System;
using System.Transactions;
using NUnit.Framework.Interfaces;

namespace GigHubIntergrationTests
{
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;
        public ActionTargets Targets
        {
            get
            {
                return ActionTargets.Test;
            }
        }

        public void AfterTest(ITest test)
        {
            _transactionScope.Dispose();
        }

        public void BeforeTest(ITest test)
        {
            _transactionScope = new TransactionScope();
        }
    }
}
