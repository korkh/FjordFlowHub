import { auth } from "@/auth";
import Heading from "../components/Heading";
import AuthTest from "./AuthTest";

export default async function Session() {
  const session = await auth();
  return (
    <div>
      <Heading title="Session Page" />
      <div className="bg-blue-200 border-2 border-blue-500">
        <h3 className="text-lg">
          Session:{" "}
          <pre className="whitespace-pre-wrap break-all">
            {JSON.stringify(session, null, 2)}
          </pre>
        </h3>
      </div>
      <div className="mt-4">
        <AuthTest />
      </div>
    </div>
  );
}
