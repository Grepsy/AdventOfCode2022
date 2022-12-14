using System.Diagnostics;

var sw = Stopwatch.StartNew();
Day14.Run1().Log();
Day14.Run2().Log();
$"{sw.ElapsedMilliseconds} ms".Log();
