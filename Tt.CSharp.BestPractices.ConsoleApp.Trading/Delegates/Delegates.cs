using System;

namespace Tt.CSharp.BestPractices.ConsoleApp.Trading.Delegates
{
    public delegate void PivotDownsideFoundDelegate(object sender, DateTime date);

    public delegate void PivotUpsideFoundDelegate(object sender, DateTime date);
}