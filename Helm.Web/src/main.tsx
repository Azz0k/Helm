import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { PublicClientApplication } from '@azure/msal-browser';
import { MsalProvider } from '@azure/msal-react';
import {StrictMode} from "react";

export const msalConfig = {
  auth: {
    clientId: import.meta.env.VITE_ADFS_CLIENT_ID,
    knownAuthorities: [import.meta.env.VITE_ADFS_KNOWN_AUTHORITY],
    authority: import.meta.env.VITE_ADFS_AUTHORITY,
    redirectUri: import.meta.env.VITE_ADFS_REDIRECT_URL, // or your app's redirect URI
    protocolMode: import.meta.env.VITE_ADFS_PROTOCOL_MODE,
    authorityMetadata: import.meta.env.VITE_ADFS_AUTHORITY_METADATA
  },
  cache: {
    cacheLocation: "localStorage", // or sessionStorage
    storeAuthStateInCookie: false,
  },
};

const msalInstance = new PublicClientApplication(msalConfig);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <MsalProvider instance={msalInstance}>
      <App />
    </MsalProvider>
  </StrictMode>
)
