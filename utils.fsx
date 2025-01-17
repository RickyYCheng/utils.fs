[<AutoOpen>]
module Utils

#nowarn 9
#nowarn 42

open System
open FSharp.NativeInterop

let inline stackalloc<'a when 'a: unmanaged> (length: int): Span<'a> =
    let p = NativePtr.stackalloc<'a> length |> NativePtr.toVoidPtr
    Span<'a>(p, length)

// better do not use inline otherwise there will be a lot of bugs
let bitcast<'a, 'b> (x:'a) = (# "" x : 'b #)

/// target netstandard2.0
type [<AbstractClass; Sealed>] Random() = 
    static let _global = System.Random()
    [<DefaultValue; ThreadStatic>] static val mutable private _local : System.Random

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
