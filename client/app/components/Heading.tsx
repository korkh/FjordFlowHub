type Props = {
  title: string;
  subtitle?: string;
  center?: boolean;
};
export default function Heading({ title, subtitle, center }: Props) {
  return (
    <div className={center ? "text-center" : "text-start"}>
      <h1 className="text-2xl font-bold">{title}</h1>
      {subtitle && (
        <p className="font-lighttext-neutral-500 mt-2 mb-4">{subtitle}</p>
      )}
    </div>
  );
}
