[<AutoOpen>]
module Utils

let inline bitcast<'a, 'b> (x:'a) = (# "" x : 'b #)

/// target netstandard2.0
type [<AbstractClass; Sealed>] Random() = 
    static let _global = System.Random()
    [<DefaultValue; System.ThreadStatic>] static val mutable private _local : System.Random

    static member private instance() = 
        let mutable rnd = Random._local
        if rnd |> isNull then 
            let seed = lock _global _global.Next
            Random._local <- System.Random seed
            rnd <- Random._local
        rnd

    static member next() = Random.instance().Next()
    static member next(max) = Random.instance().Next(max)
    static member next(min, max) = Random.instance().Next(min, max)

    static member nextF() = Random.instance().NextDouble()
    static member nextBs(buffer:byte[]) = Random.instance().NextBytes(buffer)
