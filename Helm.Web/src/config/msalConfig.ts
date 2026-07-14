import {type Configuration, PublicClientApplication} from '@azure/msal-browser';

const msalConfig : Configuration = {
  auth:{
    clientId: import.meta.env.VITE_ADFS_CLIENT_ID,
    knownAuthorities: [import.meta.env.VITE_ADFS_KNOWN_AUTHORITY],
    authority: import.meta.env.VITE_ADFS_AUTHORITY,
    redirectUri: import.meta.env.VITE_ADFS_REDIRECT_URL,
    authorityMetadata: import.meta.env.VITE_ADFS_AUTHORITY_METADATA,
  },
  cache: {
    cacheLocation: 'sessionStorage',
  },
  system:{
    protocolMode: import.meta.env.VITE_ADFS_PROTOCOL_MODE,
  }
};


export const msalInstance = new PublicClientApplication(msalConfig);