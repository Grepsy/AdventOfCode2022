using System.Diagnostics;

var sw = Stopwatch.StartNew();
Day13.Run1().Log();
Day13.Run2().Log();
$"{sw.ElapsedMilliseconds} ms".Log();
