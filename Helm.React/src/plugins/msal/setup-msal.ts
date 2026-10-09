import { msalInstance } from '@/config/msalConfig.ts';

export const setupMSAL = async () => {
  try{
    await msalInstance.initialize();
  }
  catch(error){
    console.error('MSAL initialization error: ', error);
  }
}