using System.Diagnostics;

var sw = Stopwatch.StartNew();
Day12.Run1().Log();
Day12.Run2().Log();
$"{sw.ElapsedMilliseconds} ms".Log();
