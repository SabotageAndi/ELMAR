namespace net.the_engineers.elmar.test

open FsSql
open FsUnit

module testinfrastructure =

    let checkSqlParameter (name : string) (value : obj) (parameter : Sql.Parameter) =
        obj.Equals(parameter.ParameterName,name) && obj.Equals(parameter.Value,value)

    let checkForSqlParameter (name : string) (value : obj)  parameters =
        parameters |> Seq.exists (fun i -> checkSqlParameter name value i) |> should equal true

    
        


