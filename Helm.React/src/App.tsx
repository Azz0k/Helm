import "./App.css";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import {useAuth} from "@/hooks/useAuth.ts";
import {observer} from "mobx-react";
import {configure} from "mobx";
import {rootStore} from "@/stores/root-store.ts";
import {Button} from "@base-ui/react";
import {useEffect} from "react";
import {RouterProvider} from "@tanstack/react-router";
import {router} from "@/router/router.tsx";

configure({
  enforceActions: "never",
});

export const App = observer(()=> {
  const queryClient = new QueryClient();
  const {login, handleRedirect } = useAuth();
  const handleLogin = async () => {
    await login();
  };
  useEffect(()=>{
    void handleRedirect();
  },[]);

  return (
    <QueryClientProvider client={queryClient}>
      {
        rootStore.authStore.isRedirectDone &&
        rootStore.authStore.isAuthenticated ?
          (<RouterProvider router={router} />)
          :
          (<Button onClick={handleLogin} />)
      }
    </QueryClientProvider>
  )
});


