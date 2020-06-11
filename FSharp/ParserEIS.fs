namespace ParserEIC

module ParserSite =
    open FSharp.Data
    let private  request = "https://zakupki.gov.ru/223/purchase/public/purchase/info/journal.html?regNumber={num}"

    let private  urlRequest num =
            let url = request.Replace("{num}",num)
            url

    let private  results (url:string) = 
        HtmlDocument.Load(url)

    let private  tovar num = 
        let array = 
            (results (urlRequest num)).Descendants["tr"]
                    |> Seq.choose(fun x -> x.TryGetAttribute("class") 
                                                            |> Option.filter (fun op -> x.AttributeValue(op.Name()) = "odd" || x.AttributeValue(op.Name()) = "even" )
                                                            |> Option.map (fun o -> x.InnerText().Trim())
                                                            )
        array
        
    let public  GetStringArray num = 
        tovar num
        |>Seq.toArray : string[]

    printfn "Массив"
    printfn "%A" (GetStringArray "32009171407")

   
