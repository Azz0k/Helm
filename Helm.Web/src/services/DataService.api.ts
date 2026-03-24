import {rootStore} from "../store/root-store.ts";

export const FetchData = (url:string) => {
  return fetch(url, {
    method: 'GET',
    headers: {
      'Authorization': `Bearer ${rootStore.accessToken}`,
    }
  })
};
export const UpdateData = async (url:string, body:string) => {
  return  fetch(url, {
    method: 'PUT',
    body: body,
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${rootStore.accessToken}`,
    }
  });
};
export const DeleteData = async (url:string) => {
  return fetch(url, {
    method: 'DELETE',
    headers: {
      'Authorization': `Bearer ${rootStore.accessToken}`,
    }
  });
};

export const AddData = (url:string, body:string) => {
  return fetch(url, {
    method: 'POST',
    body: body,
    headers: {
      'Authorization': `Bearer ${rootStore.accessToken}`,
      'Content-Type': 'application/json'
    }
  });
}