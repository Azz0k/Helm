import './App.css'
import {useEffect} from "react";
import {Authenticate} from "./services/Authenticate.api.ts";

function App() {
  useEffect(()=>{
    Authenticate("").then((result)=>console.log(result));
  },[])
  return (
    <>

    </>
  )
}

export default App
