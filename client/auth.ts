import NextAuth, { Profile } from "next-auth";
import { OIDCConfig } from "next-auth/providers";
import DuendeIDS6Provider from "next-auth/providers/duende-identity-server6";

export const { handlers, signIn, signOut, auth } = NextAuth({
  providers: [
    //same as the client id in our identity server config
    DuendeIDS6Provider({
      id: "id-server",
      clientId: "nextApp",
      clientSecret: "secret",
      issuer: process.env.IDENTITY_URL,
      authorization: {
        params: { scope: "openid profile freightApp" },
        //url: "http://localhost:5000/connect/authorize" Identity server,
        url: process.env.IDENTITY_URL + "/connect/authorize",
      },
      token: {
        url: `${process.env.IDENTITY_URL_INTERNAL}/connect/token`,
      },
      userinfo: {
        url: `${process.env.IDENTITY_URL_INTERNAL}/connect/userinfo`,
      },
      idToken: true,
    } as OIDCConfig<Profile>),
  ],
  callbacks: {
    async redirect({ url, baseUrl }) {
      return url.startsWith(baseUrl) ? url : baseUrl;
    },
    async authorized({ auth }) {
      return !!auth; // Only allow access to authenticated users
    },
    async jwt({ token, profile, account }) {
      if (account && account.access_token) {
        token.accessToken = account.access_token;
      }
      // Persist the access token in the JWT token
      if (profile) {
        token.username = profile.username;
      }
      return token;
    },
    async session({ session, token }) {
      if (token) {
        session.user.username = token.username as string;
        session.accessToken = token.accessToken as string;
      }
      return session;
    },
  },
});
