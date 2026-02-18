"use client";

import { Label } from "flowbite-react";
import DatePicker, { DatePickerProps } from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useController, UseControllerProps } from "react-hook-form";

// English: Define props extending useController to integrate with react-hook-form
type Props = {
  label: string;
  type?: string;
  showLabel?: boolean;
  showTimeSelect?: boolean;
} & UseControllerProps &
  DatePickerProps;

export default function DateInput(props: Props) {
  const { fieldState, field } = useController({ ...props });

  return (
    <div className="block">
      {props.showLabel && (
        <div className="mb-2 block">
          <Label htmlFor={field.name}>{props.label}</Label>
        </div>
      )}
      <DatePicker
        {...props}
        {...field}
        onChange={(value: any) => field.onChange(value)}
        selected={field.value ? new Date(field.value) : null}
        dateFormat="dd.MM.yyyy HH:mm"
        timeFormat="HH:mm"
        timeIntervals={15}
        placeholderText={props.label}
        className={`
                        rounded-lg
                        w-full
                        border
                        border-gray-600
                        p-2
                        flex flex-col
                        ${
                          fieldState.error
                            ? "bg-red-50 border-red-500 text-red-900"
                            : !fieldState.invalid && fieldState.isDirty
                              ? "bg-green-50 border-green-500 text-green-900"
                              : ""
                        }   
                    `}
      />
      {fieldState.error && (
        <div className="mt-2 text-sm text-red-600">
          {fieldState.error.message}
        </div>
      )}
    </div>
  );
}
