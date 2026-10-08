
export const FetchData = (url:string, accessToken:string) => {
  return fetch(url, {
    method: 'GET',
    headers: {
      'Authorization': `Bearer ${accessToken}`,
    }
  })
};
export const UpdateData = async (url:string, body:string, accessToken:string) => {
  return  fetch(url, {
    method: 'PUT',
    body: body,
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${accessToken}`,
    }
  });
};
export const DeleteData = async (url:string, accessToken:string) => {
  return fetch(url, {
    method: 'DELETE',
    headers: {
      'Authorization': `Bearer ${accessToken}`,
    }
  });
};

export const AddData = (url:string, body:string, accessToken:string) => {
  return fetch(url, {
    method: 'POST',
    body: body,
    headers: {
      'Authorization': `Bearer ${accessToken}`,
      'Content-Type': 'application/json'
    }
  });
}