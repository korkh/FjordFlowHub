import { auth } from "@/auth";

const baseUrl = "http://localhost:6001/";

function getFullUrl(url: string): string {
  const cleanBaseUrl = baseUrl.endsWith("/") ? baseUrl : `${baseUrl}/`;
  const cleanUrl = url.startsWith("/") ? url.substring(1) : url;
  return `${cleanBaseUrl}${cleanUrl}`;
}

export async function get(url: string) {
  const requestOptions = {
    method: "GET",
    headers: await getHeaders(),
  };
  return await fetch(getFullUrl(url), requestOptions).then(handleResponse);
}

export async function post(url: string, body: object) {
  const requestOptions = {
    method: "POST",
    headers: await getHeaders(),
    body: JSON.stringify(body),
  };
  return await fetch(getFullUrl(url), requestOptions).then(handleResponse);
}

export async function put(url: string, body: object) {
  const requestOptions = {
    method: "PUT",
    headers: await getHeaders(),
    body: JSON.stringify(body),
  };
  return await fetch(getFullUrl(url), requestOptions).then(handleResponse);
}

export async function del(url: string) {
  const requestOptions = {
    method: "DELETE",
    headers: await getHeaders(),
  };
  return await fetch(getFullUrl(url), requestOptions).then(handleResponse);
}

// Helper function to handle responses
async function handleResponse(response: Response) {
  const text = await response.text();
  let data;

  try {
    data = text ? JSON.parse(text) : null;
  } catch {
    data = text;
  }

  if (response.ok) {
    return data || response.statusText;
  } else {
    const error = {
      status: response.status,
      message:
        typeof data === "object" && data !== null && "message" in data
          ? data.message
          : data || response.statusText,
    };
    return { error };
  }
}

async function getHeaders(): Promise<Headers> {
  const session = await auth();
  const headers = new Headers();
  headers.set("Content-Type", "application/json");

  if (session) {
    headers.set("Authorization", `Bearer ${session.accessToken}`);
  }
  return headers;
}

export const fetchWrapper = {
  get,
  post,
  put,
  del,
};
