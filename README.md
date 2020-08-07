# ResolveMe

C# .NET Core solution for parsing and resolving mathematical expressions. Project has own expression context free grammar defined by Extended Backus–Naur form. Grammar is parser using **Amy** library.

Actual version supports:

* operators +,-,*,/,^, %
* Functions: sin, cos, argsin, argcos, tan, argtang, logx, ln, min, max, sum, avg
* Variables: PI, e,
* Using of Recursive Functions `sin(cos(sin(cos(0.1))))`
* Custom Context
* Return expression in tokens
* Return expression in tokens postfix notation
* And much more..

## How to use it
It's simply to use, just create new instance of MathCalculator class and call Calculate method with arguments. MathCalculator use default context which has defined all basic functions.

```C#
var calculator = new MathCalculator();
var expression = "cos(24-23.8*0.2)";
var result = calculator.Calculate<double>(expression);
```

### Custom Variable
If you want to use custom variables just add it.
```C#
var calculator = new MathCalculator();
calculator.Context.AddVariable("ab", (double)10);
var expression = "max(25,1)+45-ab*bc+12";
var result = calculator.Calculate<double>(expression);
```

### Custom Function
Example of defining custom standart deviation function.
```C#
var calculator = new MathCalculator();
var stdv = new Func<object[], object>((args) =>
{
  calculator.Context.TryGetFunction("avg", out var avgFunct);
  var avg = (double)avgFunct(args);
  var result = (double)0;
  foreach (var arg in args)
  {
      if (arg is double number)
      {
        var temp = number - avg;
        result += Math.Pow(temp, 2);
        continue;
      }

      throw new ArgumentException("args", "Function expects double");
   }
   return Math.Sqrt(result / (double)args.Length);
});
            
calculator.Context.TryAddFunction("stdv", stdv);
var expression = "stdv(25,1,6,15,17)";
var result = this.calculator.Calculate<double>(expression);
```

### Custom Context
You can define your own context which has to inherite from IContext interface and then pass it as argument to calculator constructor.
```C#
var context = new MyOwnContext();
var calculator = new MathCalculator(context);
calculator.Context.AddVariable("ab", (double)10);
var expression = "max(25,1)+45-ab*bc+12";
var result = calculator.Calculate<double>(expression);
```
