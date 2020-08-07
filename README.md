# ResolveMe

C# .NET Core solution for parsing and resolving mathematical expressions. Project has own expression context free grammar defined by Extended Backus–Naur form. Grammar is parser using **Amy** library.

Actual version supports:

* operators +,-,*,/,^
* Functions: sin, cos, argsin, argcos, tan, argtang, log10, min, max
* Variables: PI, e,
* Using of Recursive Functions `sin(cos(sin(cos(0.1))))`
* Custom Context
* Return expression in tokens
* Return expression in tokens postfix notation
* And much more..

##Performance

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


### Custom Context
You can define your own context which has to inherite from IContext interface and then pass it as argument to calculator constructor.
```C#
var context = new MyOwnContext();
var calculator = new MathCalculator(context);
calculator.Context.AddVariable("ab", (double)10);
var expression = "max(25,1)+45-ab*bc+12";
var result = calculator.Calculate<double>(expression);
```
