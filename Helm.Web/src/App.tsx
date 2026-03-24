import './App.css'
import { useMsal } from "@azure/msal-react";
import {useEffect, useState} from "react";
import { InteractionRequiredAuthError, InteractionStatus} from "@azure/msal-browser";
import {configure} from "mobx";
import {observer} from "mobx-react";
import {rootStore} from "./store/root-store.ts";

configure({
  enforceActions: 'never',
});

export  const  App = observer(()=> {
  const { instance, inProgress, accounts } = useMsal();
  const [apiData] = useState(null);
  useEffect(() => {
    const accessTokenRequest = {
      scopes: ["user.read"],
      account: accounts[0],
    };
    if (inProgress === InteractionStatus.None) {
      instance
        .acquireTokenSilent(accessTokenRequest)
        .then((accessTokenResponse) => {
          /*
          callApi(accessToken).then((response) => {
            setApiData(response);
          });*/
          //console.log(accessTokenResponse);
          rootStore.handleLogon(accessTokenResponse.account.username, accessTokenResponse.accessToken);
        })
        .catch((error) => {
          if (error instanceof InteractionRequiredAuthError) {
            instance.acquireTokenRedirect(accessTokenRequest).then();
          }
          console.log(error);
          rootStore.handleLogout();
        });
    }
  }, [instance, accounts, inProgress, apiData]);
   return(
    <>
      {rootStore.isLoggedIn && (
        <div className="button-div">
          {rootStore.userName}
        </div>
      )
      }
    </>
  );
});


export default App
