import { useState } from "react";
import "../styles/select.css"

export function UseSelect({label, options}) {
    const [value, setValue] = useState("");

    const select = <div className="select-container">
        {label != null
            ? <label>{label}</label>
            : ""
        }
        <select value={value} onChange={e => setValue(e.target.value)}>
            {options.map((option) => (
                <option value={option.value}>{option.label}</option>
            ))}
        </select>
    </div>;
    return [value, setValue, select];
}