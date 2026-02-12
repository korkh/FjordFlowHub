import withFlowbiteReact from "flowbite-react/plugin/nextjs";
import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  logging: {
    fetches: {
      fullUrl: true,
    },
  },
  images: {
    remotePatterns: [
      { hostname: "tse2.mm.bing.net" },
      { hostname: "tse4.mm.bing.net" },
      { hostname: "cdn.pixabay.com" },
      { hostname: "media.sciencephoto.com" },
      { hostname: "scu-bucket-3.oss-eu-central-1.aliyuncs.com" },
    ].map((pattern) => ({
      protocol: "https",
      port: "",
      pathname: "/**",
      ...pattern,
    })),
  },
};

export default withFlowbiteReact(nextConfig);
